using System;
using System.Collections.Generic;
using System.Linq;

public static class RatingManager
{
    public static Action OnRatingChanged = delegate { };
    public static Dictionary<int, RatingEntity> Rating = new Dictionary<int, RatingEntity>();

    public static void Init(RatingEntity[] ratingEntities)
    {
        Rating = ratingEntities.ToDictionary(x => x.Id, x => x);
     
        OnRatingChanged();
    }

    public static void Update(RatingEntity entity)
    {
        Rating[entity.Id] = entity;

        OnRatingChanged();
    }

    public static void UpdateKillAndDeath(int killer, int died)
    {
        Rating[killer].Killed++;
        Rating[died].Died++;

        OnRatingChanged();
    }

    public static void UpdateDeath(int died)
    {
        Rating[died].Died++;

        OnRatingChanged();
    }
}
