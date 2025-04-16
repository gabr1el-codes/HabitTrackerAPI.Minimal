using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Habits.Contracts.Responses;
public class HabitResponse
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public bool IsCompleted { get; init; }
    public DateTime? CompletedAt { get; init; }
}   
