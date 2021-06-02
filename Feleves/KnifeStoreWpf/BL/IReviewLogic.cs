using KnifeStoreWpf.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KnifeStoreWpf.BL
{
    public interface IReviewLogic
    {
        void AddReview(IList<Review> list, string selectedKnifeId);
        void DelReview(IList<Review> list, Review review);
        void ModReview(Review reviewToModify);
        IList<Review> GetAllReviewsForKnife(string knifeId);
    }
}
