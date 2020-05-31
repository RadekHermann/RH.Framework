using System;
using System.ComponentModel.DataAnnotations;

namespace RH.App.Infrastructure.Data
{
    public interface IEntity
    {
        [Key] Guid Id { get; set; }
    }
}
