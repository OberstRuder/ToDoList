﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace ToDo.Models
{
    public class Todo
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Please enter description")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter due date")]
        public DateTime? DueDate { get; set; }

        [Required(ErrorMessage = "Please select a category")]
        public string CategoryId { get; set; } = string.Empty;

        [ValidateNever]
        public Category Category { get; set; } = null!;

        [Required(ErrorMessage = "Please select a user")]
        public string UserId { get; set; } = string.Empty;

        [ValidateNever]
        public User User { get; set; } = null!;

        [Required(ErrorMessage ="Select a status")]
        public string StatusId { get; set; } = string.Empty;

        [ValidateNever]
        public Status Status { get; set; } = null!;

        public bool Overdue => StatusId == "open" && DueDate < DateTime.Today;
    }
}
