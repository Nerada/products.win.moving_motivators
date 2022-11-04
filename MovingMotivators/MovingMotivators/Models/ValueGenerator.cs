using System.Collections.Generic;
using System.Collections.Immutable;

namespace MovingMotivators.Models;

public class ValueGenerator
{
    private readonly List<User> _generatedUsers = new();

    public ValueGenerator()
    {
        User mi = new(_generatedUsers.Count, "Mi");
        mi.AddMotivators(new List<MotivatorValue>
        {
            new(Motivator.Acceptance, 5),
            new(Motivator.Curiosity, 9),
            new(Motivator.Freedom, 1),
            new(Motivator.Status, 2),
            new(Motivator.Goal, 7),
            new(Motivator.Honor, 8),
            new(Motivator.Mastery, 10),
            new(Motivator.Order, 3),
            new(Motivator.Power, 6),
            new(Motivator.Relatedness, 4)
        });
        _generatedUsers.Add(mi);

        User ra = new(_generatedUsers.Count, "Ra");
        ra.AddMotivators(new List<MotivatorValue>
        {
            new(Motivator.Acceptance, 4),
            new(Motivator.Curiosity, 5),
            new(Motivator.Freedom, 3),
            new(Motivator.Status, 8),
            new(Motivator.Goal, 1),
            new(Motivator.Honor, 2),
            new(Motivator.Mastery, 7),
            new(Motivator.Order, 6),
            new(Motivator.Power, 9),
            new(Motivator.Relatedness, 10)
        });
        _generatedUsers.Add(ra);

        User ja = new(_generatedUsers.Count, "Ja");
        ja.AddMotivators(new List<MotivatorValue>
        {
            new(Motivator.Acceptance, 6),
            new(Motivator.Curiosity, 8),
            new(Motivator.Freedom, 10),
            new(Motivator.Status, 1),
            new(Motivator.Goal, 2),
            new(Motivator.Honor, 5),
            new(Motivator.Mastery, 7),
            new(Motivator.Order, 4),
            new(Motivator.Power, 3),
            new(Motivator.Relatedness, 9)
        });
        _generatedUsers.Add(ja);

        User yv = new(_generatedUsers.Count, "Yv");
        yv.AddMotivators(new List<MotivatorValue>
        {
            new(Motivator.Acceptance, 10),
            new(Motivator.Curiosity, 1),
            new(Motivator.Freedom, 9),
            new(Motivator.Status, 5),
            new(Motivator.Goal, 2),
            new(Motivator.Honor, 6),
            new(Motivator.Mastery, 4),
            new(Motivator.Order, 7),
            new(Motivator.Power, 8),
            new(Motivator.Relatedness, 3)
        });
        _generatedUsers.Add(yv);

        User ka = new(_generatedUsers.Count, "Ka");
        ka.AddMotivators(new List<MotivatorValue>
        {
            new(Motivator.Acceptance, 4),
            new(Motivator.Curiosity, 10),
            new(Motivator.Freedom, 8),
            new(Motivator.Status, 2),
            new(Motivator.Goal, 3),
            new(Motivator.Honor, 5),
            new(Motivator.Mastery, 9),
            new(Motivator.Order, 7),
            new(Motivator.Power, 6),
            new(Motivator.Relatedness, 1)
        });
        _generatedUsers.Add(ka);

        User sa = new(_generatedUsers.Count, "Sa");
        sa.AddMotivators(new List<MotivatorValue>
        {
            new(Motivator.Acceptance, 1),
            new(Motivator.Curiosity, 8),
            new(Motivator.Freedom, 5),
            new(Motivator.Status, 6),
            new(Motivator.Goal, 2),
            new(Motivator.Honor, 3),
            new(Motivator.Mastery, 9),
            new(Motivator.Order, 4),
            new(Motivator.Power, 7),
            new(Motivator.Relatedness, 10)
        });
        _generatedUsers.Add(sa);

        User gr = new(_generatedUsers.Count, "Gr");
        gr.AddMotivators(new List<MotivatorValue>
        {
            new(Motivator.Acceptance, 5),
            new(Motivator.Curiosity, 6),
            new(Motivator.Freedom, 4),
            new(Motivator.Status, 3),
            new(Motivator.Goal, 8),
            new(Motivator.Honor, 7),
            new(Motivator.Mastery, 10),
            new(Motivator.Order, 1),
            new(Motivator.Power, 9),
            new(Motivator.Relatedness, 2)
        });
        _generatedUsers.Add(gr);

        User jo = new(_generatedUsers.Count, "Jo");
        jo.AddMotivators(new List<MotivatorValue>
        {
            new(Motivator.Acceptance, 10),
            new(Motivator.Curiosity, 3),
            new(Motivator.Freedom, 1),
            new(Motivator.Status, 4),
            new(Motivator.Goal, 8),
            new(Motivator.Honor, 6),
            new(Motivator.Mastery, 5),
            new(Motivator.Order, 2),
            new(Motivator.Power, 7),
            new(Motivator.Relatedness, 9)
        });
        _generatedUsers.Add(jo);
    }

    public ImmutableList<User> GetData() => _generatedUsers.ToImmutableList();
}
