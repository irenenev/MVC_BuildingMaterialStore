﻿using Braintree; //nuget package
using Microsoft.Extensions.Options;

namespace Store_Utility.BrainTree
{
    public class BrainTreeGate : IBrainTreeGate
    {
        public BrainTreeSettings _options { get; set; }
        private IBraintreeGateway brainTreeGateWay { get; set; }
        public BrainTreeGate(IOptions<BrainTreeSettings> options)
        {
            _options = options.Value;
        }

        public IBraintreeGateway CreateGateway()
        {
            return new BraintreeGateway(_options.Environment, _options.MerchantId, _options.PublicKey, _options.PrivateKey);
        }

        public IBraintreeGateway GetGateway()
        {
            if (brainTreeGateWay == null)
                brainTreeGateWay = CreateGateway();
            return brainTreeGateWay;
        }
    }
}
