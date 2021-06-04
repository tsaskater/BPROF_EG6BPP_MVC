using KnifeStoreWpf.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KnifeStoreWpf.BL
{
    public interface IReviewLogic
    {
        void AddReview(IList<Review> list, string selectedKnifeId,string token);
        void DelReview(IList<Review> list, Review review,string token);
        void ModReview(Review reviewToModify,string token);
        IList<Review> GetAllReviewsForKnife(string knifeId,string token);
    }
}
