using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Habits.Contracts.Responses;
public class HabitsResponse
{
    public IEnumerable<HabitResponse> Items { get; init; } = Enumerable.Empty<HabitResponse>();
}
