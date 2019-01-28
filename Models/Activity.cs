using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace firstbelt.Models
{
    public class Activity
    {
        [Key]
        public int ActivityId { get; set; }

        [Display(Name="Title")]
        [Required (ErrorMessage="What's your name again?")]
        [MinLength(2)]
        public string Title { get; set; }

        [Display(Name="Time")]
        [Required (ErrorMessage="What time is your event?")]
        public DateTime Time { get; set; }

        [Display(Name="Date")]
        [Required (ErrorMessage="When is it? All dates must me in the future.")]
        public DateTime Date { get; set; }

        [Display(Name="Duration")]
        [Required]
        public int Duration { get; set; }

        [Required]
        public string DurType { get; set; }

        [Display(Name="Description")]
        [Required (ErrorMessage="Please provide a description. Descriptions must be at least 10 characters in length.")]
        [MinLength(10)]
        public string Desc { get; set; }

        public User User { get; set; }

        public int UserId { get; set; }

        public List<Participant> Participants { get; set; }
    }
}