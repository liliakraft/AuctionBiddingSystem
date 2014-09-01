using System.Collections.Generic;
using System.Linq;
using AuctionBiddingSystem;
using AuctionBiddingSystem.Bid;
using AuctionBiddingSystem.Bid.Interaces.Services;
using AuctionBiddingSystem.Interfaces.Services;
using Moq;
using NUnit.Framework;

namespace AuctionBiddingSystemTests
{
    [TestFixture]
    public class WinningBidSetterTests
    {
        private List<AssetBid> bids;
        private WinningBidSetter winningBidSetter;
        private Mock<IBidService> bidServiceMock;
        private const int bidRoundId = 1;

        [SetUp]
        public void SetUp()
        {
            bids = new List<AssetBid>
                {
                    new AssetBid {Asset = new Asset {Name = "Radio spectrum Augsburg"}, BiddingPrice = 100},
                    new AssetBid {Asset = new Asset {Name = "Radio spectrum Munich"}, BiddingPrice = 1000},
                    new AssetBid {Asset = new Asset {Name = "Radio spectrum Passau"}, BiddingPrice = 10}
                };
            bidServiceMock = new Mock<IBidService>();
            bidServiceMock.Setup(i => i.GetBidsForBiddingRound(It.IsAny<int>())).Returns(bids);
            winningBidSetter = new WinningBidSetter(bidServiceMock.Object);
        }

        [Test]
        public void Should_set_is_winning_bid_to_true_for_highest_bid()
        {
            winningBidSetter.SetWinningBid(bidRoundId);

            Assert.That(bids.Single(i => i.Asset.Name == "Radio spectrum Munich").IsWinningBid, Is.EqualTo(true));
        }

        [Test]
        public void Should_set_is_winning_bid_to_false_if_not_highest_bid()
        {
            winningBidSetter.SetWinningBid(bidRoundId);

            Assert.That(bids.Single(i => i.Asset.Name == "Radio spectrum Augsburg").IsWinningBid, Is.EqualTo(false));
        }

        [Test]
        public void Should_get_bids_from_bid_service_when_setting_winning_bid()
        {

            winningBidSetter.SetWinningBid(It.IsAny<int>());

            bidServiceMock.Verify(i => i.GetBidsForBiddingRound(It.IsAny<int>()), Times.Once);
        }
    }
}
