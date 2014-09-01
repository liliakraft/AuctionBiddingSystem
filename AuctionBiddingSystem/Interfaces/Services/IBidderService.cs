using System.Collections.Generic;

namespace AuctionBiddingSystem.Interfaces.Services
{
    public interface IBidderService
    {
        IEnumerable<Bidder> GetBidders();
    }
}