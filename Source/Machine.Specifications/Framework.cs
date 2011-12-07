using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Machine.Specifications
{
  public delegate void Given();
  public delegate void When();
  public delegate void Then();
  public delegate void Behaves_like<TBehavior>();

  public delegate void Cleanup();
}