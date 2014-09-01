using AuctionBiddingSystem;
using AuctionBiddingSystem.Bid;
using NUnit.Framework;

namespace AuctionBiddingSystemTests
{
    [TestFixture]
    public class BidValidatorTests
    {
        private AssetBid assetBid;
        private decimal assetPrice;
        private decimal biddingPrice;
        private BidValidator bidValidator;

        [SetUp]
        public  void SetUp()
        {
            bidValidator = new BidValidator();
        }

        public void CreateBid()
        {
            assetBid = new AssetBid
                {
                    BiddingPrice = biddingPrice,
                    Asset = new Asset {Price = assetPrice, Name = "Radio spectrum Munich"}
                };
        }

        [Test]
        public void Should_return_false_if_bidding_price_lower_than_asset_initial_price()
        {
            biddingPrice = 10000;
            assetPrice = 10000.50m;
            CreateBid();

            Assert.False(bidValidator.ValidateBidNotBelowAssetInitialPrice(assetBid));
        }

        [Test]
        public void Should_return_true_if_bidding_price_higher_than_asset_price()
        {
            biddingPrice = 10001;
            assetPrice = 10000.50m;
            CreateBid();

            Assert.True(bidValidator.ValidateBidNotBelowAssetInitialPrice(assetBid));
        }

        public void Should_return_true_if_bidding_price_is_equal_to_asset_price()
        {
            biddingPrice = 10000.50m;
            assetPrice = 10000.50m;

            Assert.True(bidValidator.ValidateBidNotBelowAssetInitialPrice(assetBid));
        }
    }
}
