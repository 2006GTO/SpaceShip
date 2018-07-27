using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using spaceship.Models;
using System.Globalization;
using System.Threading;


namespace spaceship.Models
{
    public static class MyExtensionMethods
    {
        public static DateTime Tomorrow(this DateTime date)
        {
            return date.AddDays(1);
        }    

public static String TimeAgo(this DateTime dateTime)
{
    string result = string.Empty;
    var timeSpan = DateTime.Now.Subtract(dateTime);

    if (timeSpan <= TimeSpan.FromSeconds(60))
    {
        result = string.Format("{0} seconds ago", timeSpan.Seconds);
    }
    else if (timeSpan <= TimeSpan.FromMinutes(60))
    {
        result = timeSpan.Minutes > 1 ? 
            String.Format("about {0} minutes ago", timeSpan.Minutes) :
            "about a minute ago";
    }
    else if (timeSpan <= TimeSpan.FromHours(24))
    {
        result = timeSpan.Hours > 1 ? 
            String.Format("about {0} hours ago", timeSpan.Hours) : 
            "about an hour ago";
    }
    else if (timeSpan <= TimeSpan.FromDays(30))
    {
        result = timeSpan.Days > 1 ? 
            String.Format("about {0} days ago", timeSpan.Days) : 
            "yesterday";
    }
    else if (timeSpan <= TimeSpan.FromDays(365))
    {
        result = timeSpan.Days > 30 ? 
            String.Format("about {0} months ago", timeSpan.Days / 30) : 
            "about a month ago";
    }
    else
    {
        result = timeSpan.Days > 365 ? 
            String.Format("about {0} years ago", timeSpan.Days / 365) : 
            "about a year ago";
    }

    return result;
}


    }
}