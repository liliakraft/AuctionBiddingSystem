using System;
using System.Collections.Generic;
using AuctionBiddingSystem;
using AuctionBiddingSystem.Bid;
using AuctionBiddingSystem.Bid.Interaces.Services;
using AuctionBiddingSystem.Interfaces.Services;
using Moq;
using NUnit.Framework;

namespace AuctionBiddingSystemTests
{
    [TestFixture]
    public class SubmittedBidsValidatorTests
    {
        private Mock<IBidderService> bidderServiceMock;
        private Mock<IBidService> bidServiceMock;
        private List<AssetBid> assetBids;
        private SubmittedBidsValidator submittedBidsValidator;

        [SetUp]
        public void SetUp()
        {
            assetBids = new List<AssetBid>();
            var bidders = new List<Bidder>
                {
                    new Bidder {Name = "Bidder 1"},
                    new Bidder {Name = "Bidder 2"},
                    new Bidder {Name = "Bidder 3"}
                };

            bidderServiceMock = new Mock<IBidderService>();
            bidderServiceMock.Setup(i => i.GetBidders()).Returns(bidders);
            bidServiceMock = new Mock<IBidService>();
        }

        public void CreateSubmittedBidsValidator()
        {
            bidServiceMock.Setup(i => i.GetBidsForBiddingRound(It.IsAny<int>())).Returns(assetBids);
            submittedBidsValidator = new SubmittedBidsValidator(bidderServiceMock.Object, bidServiceMock.Object);            
        }

        [Test]
        public void Should_return_true_if_all_bidders_submitted_a_bid()
        {
            assetBids = new List<AssetBid>
            {
                new AssetBid { Bidder = new Bidder{Name = "Bidder 1"}},
                new AssetBid { Bidder = new Bidder{Name = "Bidder 2"}},
                new AssetBid { Bidder = new Bidder{Name = "Bidder 3"}}
            };
            CreateSubmittedBidsValidator();

            Assert.True(submittedBidsValidator.ValidateThatAllBiddersSubmittedABid(It.IsAny<int>()));
        }

        [Test]
        public void Should_return_false_if_not_all_bidders_submitted_a_bid()
        {
            assetBids = new List<AssetBid>
                {
                    new AssetBid { Bidder = new Bidder{Name = "Bidder 1"}},
                    new AssetBid { Bidder = new Bidder{Name = "Bidder 2"}},
                };
            CreateSubmittedBidsValidator();

            Assert.False(submittedBidsValidator.ValidateThatAllBiddersSubmittedABid(It.IsAny<int>()));
        }

        [Test]
        public void Should_get_bidders_from_bid_service_when_validating_submitted_bids()
        {
            CreateSubmittedBidsValidator();

            submittedBidsValidator.ValidateThatAllBiddersSubmittedABid(It.IsAny<int>());

            bidderServiceMock.Verify(i=>i.GetBidders(), Times.Once);
        }

        [Test]
        public void Should_get_bids_from_bid_service_when_validating_submitted_bids()
        {
            CreateSubmittedBidsValidator();

            submittedBidsValidator.ValidateThatAllBiddersSubmittedABid(It.IsAny<int>());

            bidServiceMock.Verify(i => i.GetBidsForBiddingRound(It.IsAny<int>()), Times.Once);
        }
    }
}
