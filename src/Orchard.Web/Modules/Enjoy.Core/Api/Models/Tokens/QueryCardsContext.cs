



namespace Enjoy.Core.ApiModels
{
    using Enjoy.Core.EnjoyModels;
    using Newtonsoft.Json;
    /// <summary>
    /// The query context of  query cards of merchant
    /// </summary>
    public class QueryCardsContext
    {
        /// <summary>
        /// the id of merchant in SharingV platform
        /// </summary>
        public int MerchantId { get; set; }
        /// <summary>
        /// assign what card types you want query
        /// </summary>
        public CardTypes[] Types { get; set; }
    }
}