﻿using MaitlandsInterfaceFramework.Core.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Sparkle.LinkedInNET;
using Sparkle.LinkedInNET.Common;
using Sparkle.LinkedInNET.Organizations;
using Sparkle.LinkedInNET.Profiles;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;

namespace MaitlandsInterfaceFramework.LinkedIn
{
    public static class LinkedInApiExtensions
    {
        public static LinkedInApiPaginatedResult<AdAnalyticsElement> GetAdsReporting(this LinkedInApi api, OrgEntElements company, DateTime startDate, DateTime? endDate = null)
        {
            StringBuilder apiQueryBuilder = new StringBuilder();
            apiQueryBuilder.Append("/v2/adAnalyticsV2?q=analytics&pivot=CAMPAIGN&timeGranularity=DAILY");

            // Build the startDate param into the query 
            apiQueryBuilder.Append($"&dateRange.start.day={startDate.Day}&dateRange.start.month={startDate.Month}&dateRange.start.year={startDate.Year}");

            if (endDate.HasValue)
            {
                // Build the endDate param into the query
                apiQueryBuilder.Append($"&dateRange.end.day={endDate?.Day}&dateRange.end.month={endDate?.Month}&dateRange.end.year={endDate?.Year}");
            }

            apiQueryBuilder.Append($"&companies[0]={company.OrganizationalTarget}");

            string resultJson = api.RawGetJsonQuery(apiQueryBuilder.ToString(), LinkedInApiFactory.GetUserAuthorization());
            LinkedInApiPaginatedResult<AdAnalyticsElement> result = JsonConvert.DeserializeObject<LinkedInApiPaginatedResult<AdAnalyticsElement>>(resultJson);

            return result;
        }

        public static LinkedInApiPaginatedResult<LikeElement> GetLikesForShare (this LinkedInApi api, PostShareResult share, int count = 500, int start = 0)
        {
            string apiPath = $"/v2/socialActions/{share.Activity}/likes?count={count}&start={start}";

            string likesJson = api.RawGetJsonQuery(apiPath, LinkedInApiFactory.GetUserAuthorization());
            LinkedInApiPaginatedResult<LikeElement> result = JsonConvert.DeserializeObject<LinkedInApiPaginatedResult<LikeElement>>(likesJson);

            return result;
        }

        public static LinkedInApiPaginatedResult<AdAccountElement> GetAdAccounts(this LinkedInApi api)
        {
            string adAccountsJson = api.RawGetJsonQuery("/v2/adAccountsV2?q=search", LinkedInApiFactory.GetUserAuthorization());
            LinkedInApiPaginatedResult<AdAccountElement> adAccounts = JsonConvert.DeserializeObject<LinkedInApiPaginatedResult<AdAccountElement>>(adAccountsJson);

            return adAccounts;
        }

        public static List<ProfileElement> GetProfilesByIds(this LinkedInApi api, IEnumerable<string> personIds)
        {
            if (personIds.Count() == 0)
            {
                return new List<ProfileElement>();
            }

            string personIdsConcat = String.Join(",", personIds.Select(y => $"(id:{y.Split(':').Last()})"));
            PersonList profiles = api.Profiles.GetProfilesByIds(LinkedInApiFactory.GetUserAuthorization(), personIdsConcat);
            List<ProfileElement> result = new List<ProfileElement>();

            foreach (var p in profiles.Results)
            {
                JProperty jProperty = p as JProperty;
                result.Add(jProperty.First.ToObject<ProfileElement>());
            }

            return result;
        }

        public static LinkedInApiPaginatedResult<CampaignElement> GetAdCampaigns(this LinkedInApi api, OrgEntElements company)
        {
            string campaignsJson = api.RawGetJsonQuery(
                path: $"/v2/adCampaignsV2?q=search&search.associatedEntity.values[0]={company.OrganizationalTarget}", 
                user: LinkedInApiFactory.GetUserAuthorization()
            );

            LinkedInApiPaginatedResult<CampaignElement> campaigns = JsonConvert.DeserializeObject<LinkedInApiPaginatedResult<CampaignElement>>(campaignsJson);

            return campaigns;
        }
    }
}
