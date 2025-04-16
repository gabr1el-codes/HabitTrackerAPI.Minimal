using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Habits.Contracts.Requests;
public class CreateHabitRequest
{
    public required string Name { get; init; }
    public string? Description { get; init; }
}