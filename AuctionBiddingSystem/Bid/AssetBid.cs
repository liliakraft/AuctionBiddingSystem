namespace AuctionBiddingSystem.Bid
{
    public class AssetBid
    {
        public Asset Asset { get; set; }

        public decimal BiddingPrice { get; set; }

        public Bidder Bidder { get; set; }

        public bool IsWinningBid { get; set; }
    }
}