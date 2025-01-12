using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace SmartVectorDotNet;

public partial class VectorOpTest
{
    private ITestOutputHelper Output { get; }

    public VectorOpTest(ITestOutputHelper output) => Output = output;
}
