// -----------------------------------------------
//     Author: Ramon Bollen
//      File: MovingMotivators.ViewModelBase.cs
// Created on: 20220629
// -----------------------------------------------

using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MovingMotivators.ViewModels;

/// <summary>
///     View model base class.
/// </summary>
public class ViewModelBase : INotifyPropertyChanged
{
    private static readonly ConcurrentDictionary<string, PropertyChangedEventArgs>
        CachedPropertyChangedEventArgs = new();

    public event PropertyChangedEventHandler? PropertyChanged;

    // ReSharper disable once UnusedMethodReturnValue.Global
    protected bool Set<T>(ref T field, T newValue, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, newValue)) return false;

        SetAndRaisePropertyChanged(out field, newValue, propertyName);
        return true;
    }

    // ReSharper disable once MemberCanBePrivate.Global
    protected void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
    {
        if (propertyName == null) return;

        PropertyChanged?.Invoke(this, GetCachedPropertyChangedEventArgs(propertyName));
    }

    private void SetAndRaisePropertyChanged<T>(out T field, T newValue, string? propertyName)
    {
        field = newValue;
        RaisePropertyChanged(propertyName);
    }

    private PropertyChangedEventArgs GetCachedPropertyChangedEventArgs(string propertyName)
    {
        if (!CachedPropertyChangedEventArgs.TryGetValue(propertyName, out PropertyChangedEventArgs? args))
        {
            args = new PropertyChangedEventArgs(propertyName);
            CachedPropertyChangedEventArgs.TryAdd(propertyName, args);
        }

        return args;
    }
}