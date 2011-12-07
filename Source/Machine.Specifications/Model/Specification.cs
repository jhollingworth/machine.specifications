using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Machine.Specifications.Model
{
  public class Specification
  {
    readonly string _name;
    readonly Then _then;
    readonly bool _isIgnored;
    readonly FieldInfo _fieldInfo;

    public FieldInfo FieldInfo
    {
      get { return _fieldInfo; }
    }

    public string Name
    {
      get { return _name; }
    }

    public bool IsIgnored
    {
      get { return _isIgnored; }
    }

    public Specification(string name, Then then, bool isIgnored, FieldInfo fieldInfo)
    {
      _name = name;
      _then = then;
      _isIgnored = isIgnored;
      _fieldInfo = fieldInfo;
    }

    public virtual Result Verify()
    {
      if (IsIgnored)
      {
        return Result.Ignored();
      }

      if (!IsDefined)
      {
        return Result.NotImplemented();
      }

      try
      {
        InvokeSpecificationField();
      }
      catch (Exception exception)
      {
        return Result.Failure(exception);
      }

      return Result.Pass();
    }

    public bool IsDefined
    {
      get { return _then != null; }
    }

    public bool IsExecutable
    {
      get { return IsDefined && !IsIgnored; }
    }

    protected virtual void InvokeSpecificationField()
    {
      _then.Invoke();
    }
  }
}