using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Habits.Contracts.Requests;
public class UpdateHabitRequest
{
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required bool IsCompleted { get; init; }
}
