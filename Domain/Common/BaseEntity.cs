using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Common; 

public abstract class BaseEntity<TId> {
  [Column("id")]
  [Required] public TId Id { get; init; }
}