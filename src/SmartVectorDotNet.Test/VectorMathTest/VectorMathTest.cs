using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace SmartVectorDotNet;

public partial class VectorMathTest
{
    private ITestOutputHelper Output { get; }

    public VectorMathTest(ITestOutputHelper output) => Output = output;
}
