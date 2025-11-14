using CinemaBookingSystem.Shared.Domain;

namespace CinemaBookingSystem.Modules.Showtimes.Domain.Entities;

public class PricingPolicy : AggregateRoot
{
    private readonly List<PricingRule> _rules = new();

    public string Name { get; private set; }
    public IReadOnlyList<PricingRule> Rules => _rules.AsReadOnly();

    private PricingPolicy() { }

    public static PricingPolicy Create(string name)
    {
        // Add validation for name if needed
        return new PricingPolicy { Name = name };
    }

    public void AddRule(PricingRule rule)
    {
        if (_rules.Any(r => r.SeatType == rule.SeatType && r.HallFormatType == rule.HallFormatType))
        {
            throw new DomainException("Duplicate pricing rule.");
        }
        _rules.Add(rule);
    }

    public void RemoveRule(PricingRule rule)
    {
        _rules.Remove(rule);
    }

    public PricingRule? GetRule(Enums.SeatType seatType, Enums.HallFormatType hallFormatType)
    {
        return _rules.FirstOrDefault(r => r.SeatType == seatType && r.HallFormatType == hallFormatType);
    }
}
