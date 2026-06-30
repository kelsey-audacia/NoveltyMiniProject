using Novelty.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Novelty.Services.Services;

public static class RatingHelper
{
    public static double CalculateAverageRating(List<Review> reviews)
    {
        if (reviews.Count <= 0) return 0;
        double sum = 0;
        foreach (var review in reviews)
        {
            sum += review.Rating;
        }
        return sum / reviews.Count;
    }
}
