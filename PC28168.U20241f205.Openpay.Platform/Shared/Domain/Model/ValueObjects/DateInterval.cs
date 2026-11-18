namespace PC28168.U20241f205.Openpay.Platform.Shared.Domain.Model.ValueObjects;

/// <summary>Represents an exclusive date interval for a scheduled operation.</summary>
/// <remarks>Author: Candy Milagros Vizcarra Mamani.</remarks>
public sealed record DateInterval
{
    /// <summary>Gets the first scheduled date.</summary>
    public DateOnly ScheduledFrom { get; private set; }
    /// <summary>Gets the last scheduled date.</summary>
    public DateOnly ScheduledTo { get; private set; }

    private DateInterval() { }

    /// <summary>Creates a valid date interval.</summary>
    /// <param name="scheduledFrom">Start date.</param>
    /// <param name="scheduledTo">End date.</param>
    /// <exception cref="ArgumentException">Thrown when end date is not after start date.</exception>
    public DateInterval(DateOnly scheduledFrom, DateOnly scheduledTo)
    {
        if (scheduledTo <= scheduledFrom)
            throw new ArgumentException("ScheduledTo must be later than ScheduledFrom.", nameof(scheduledTo));
        ScheduledFrom = scheduledFrom;
        ScheduledTo = scheduledTo;
    }

    /// <summary>Determines whether the interval has elapsed.</summary>
    /// <param name="referenceDate">Date used for comparison.</param>
    /// <returns>True when referenceDate is later than ScheduledTo.</returns>
    public bool HasElapsed(DateOnly referenceDate) => referenceDate > ScheduledTo;
}
