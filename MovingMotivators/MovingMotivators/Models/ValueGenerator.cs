using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovingMotivators.Models
{
    public class ValueGenerator
    {
        private List<User> _generatedUsers = new();
        public ValueGenerator()
        {
            var mi = new User(_generatedUsers.Count, "Mi");
            mi.AddMotivators(new List<MotivatorValue>() {
                new MotivatorValue(Motivator.Acceptance, 5),
                new MotivatorValue(Motivator.Curiosity, 9),
                new MotivatorValue(Motivator.Freedom, 1),
                new MotivatorValue(Motivator.Status, 2),
                new MotivatorValue(Motivator.Goal, 7),
                new MotivatorValue(Motivator.Honor, 8),
                new MotivatorValue(Motivator.Mastery, 10),
                new MotivatorValue(Motivator.Order, 3),
                new MotivatorValue(Motivator.Power, 6),
                new MotivatorValue(Motivator.Relatedness, 4),
            });
            _generatedUsers.Add(mi);

            var ra = new User(_generatedUsers.Count, "Ra");
            ra.AddMotivators(new List<MotivatorValue>() {
                new MotivatorValue(Motivator.Acceptance, 4),
                new MotivatorValue(Motivator.Curiosity, 5),
                new MotivatorValue(Motivator.Freedom, 3),
                new MotivatorValue(Motivator.Status, 8),
                new MotivatorValue(Motivator.Goal, 1),
                new MotivatorValue(Motivator.Honor, 2),
                new MotivatorValue(Motivator.Mastery, 7),
                new MotivatorValue(Motivator.Order, 6),
                new MotivatorValue(Motivator.Power, 9),
                new MotivatorValue(Motivator.Relatedness, 10),
            });
            _generatedUsers.Add(ra);

            var ja = new User(_generatedUsers.Count, "Ja");
            ja.AddMotivators(new List<MotivatorValue>() {
                new MotivatorValue(Motivator.Acceptance, 6),
                new MotivatorValue(Motivator.Curiosity, 8),
                new MotivatorValue(Motivator.Freedom, 10),
                new MotivatorValue(Motivator.Status, 1),
                new MotivatorValue(Motivator.Goal, 2),
                new MotivatorValue(Motivator.Honor, 5),
                new MotivatorValue(Motivator.Mastery, 7),
                new MotivatorValue(Motivator.Order, 4),
                new MotivatorValue(Motivator.Power, 3),
                new MotivatorValue(Motivator.Relatedness, 9),
            });
            _generatedUsers.Add(ja);

            var yv = new User(_generatedUsers.Count, "Yv");
            yv.AddMotivators(new List<MotivatorValue>() {
                new MotivatorValue(Motivator.Acceptance, 10),
                new MotivatorValue(Motivator.Curiosity, 1),
                new MotivatorValue(Motivator.Freedom, 9),
                new MotivatorValue(Motivator.Status, 5),
                new MotivatorValue(Motivator.Goal, 2),
                new MotivatorValue(Motivator.Honor, 6),
                new MotivatorValue(Motivator.Mastery, 4),
                new MotivatorValue(Motivator.Order, 7),
                new MotivatorValue(Motivator.Power, 8),
                new MotivatorValue(Motivator.Relatedness, 3),
            });
            _generatedUsers.Add(yv);

            var ka = new User(_generatedUsers.Count, "Ka");
            ka.AddMotivators(new List<MotivatorValue>() {
                new MotivatorValue(Motivator.Acceptance, 4),
                new MotivatorValue(Motivator.Curiosity, 10),
                new MotivatorValue(Motivator.Freedom, 8),
                new MotivatorValue(Motivator.Status, 2),
                new MotivatorValue(Motivator.Goal, 3),
                new MotivatorValue(Motivator.Honor, 5),
                new MotivatorValue(Motivator.Mastery, 9),
                new MotivatorValue(Motivator.Order, 7),
                new MotivatorValue(Motivator.Power, 6),
                new MotivatorValue(Motivator.Relatedness, 1),
            });
            _generatedUsers.Add(ka);

            var sa = new User(_generatedUsers.Count, "Sa");
            sa.AddMotivators(new List<MotivatorValue>() {
                new MotivatorValue(Motivator.Acceptance, 1),
                new MotivatorValue(Motivator.Curiosity, 8),
                new MotivatorValue(Motivator.Freedom, 5),
                new MotivatorValue(Motivator.Status, 6),
                new MotivatorValue(Motivator.Goal, 2),
                new MotivatorValue(Motivator.Honor, 3),
                new MotivatorValue(Motivator.Mastery, 9),
                new MotivatorValue(Motivator.Order, 4),
                new MotivatorValue(Motivator.Power, 7),
                new MotivatorValue(Motivator.Relatedness, 10),
            });
            _generatedUsers.Add(sa);

            var gr = new User(_generatedUsers.Count, "Gr");
            gr.AddMotivators(new List<MotivatorValue>() {
                new MotivatorValue(Motivator.Acceptance, 5),
                new MotivatorValue(Motivator.Curiosity, 6),
                new MotivatorValue(Motivator.Freedom, 4),
                new MotivatorValue(Motivator.Status, 3),
                new MotivatorValue(Motivator.Goal, 8),
                new MotivatorValue(Motivator.Honor, 7),
                new MotivatorValue(Motivator.Mastery, 10),
                new MotivatorValue(Motivator.Order, 1),
                new MotivatorValue(Motivator.Power, 9),
                new MotivatorValue(Motivator.Relatedness, 2),
            });
            _generatedUsers.Add(gr);
        }

        public ImmutableList<User> GetData() => _generatedUsers.ToImmutableList();
    }
}
