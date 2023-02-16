// -----------------------------------------------
//     Author: Ramon Bollen
//      File: MovingMotivators.User.cs
// Created on: 20220629
// -----------------------------------------------

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace MovingMotivators.Models;

public record User(int Id, string Name)
{
    private readonly List<MotivatorValue> _motivatorValues = new();

    public ImmutableList<MotivatorValue> MotivatorValues => _motivatorValues.ToImmutableList();

    public void AddMotivators(List<MotivatorValue> motivatorResults) => motivatorResults.ForEach(r => AddMotivator(r.Motivator, r.Value));

    private void AddMotivator(Motivator motivator, int order)
    {
        if (_motivatorValues.Where(m => m.Motivator == motivator).ToList().Count == 1) throw new ArgumentException("Motivator already defined.");

        if (_motivatorValues.Where(m => m.Value == order).ToList().Count == 1) throw new ArgumentException("There is already an item at this position.");

        _motivatorValues.Add(new MotivatorValue(motivator, order));
    }
}