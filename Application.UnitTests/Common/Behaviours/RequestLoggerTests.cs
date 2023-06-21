using Application.Audit.Commands.CreateAudit;
using Application.Interfaces;
using Moq;

namespace Application.UnitTests.Common.Behaviours;

public class RequestLoggerTests {
    private Mock<ICoralogixLogger<CreateAuditCommand>> _logger = null!;
    
    [SetUp]
    public void Setup() {
        _logger = new Mock<ICoralogixLogger<CreateAuditCommand>>();
    }
}