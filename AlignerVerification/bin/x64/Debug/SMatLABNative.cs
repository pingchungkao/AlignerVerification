/*
* MATLAB Compiler: 4.14 (R2010b)
* Date: Mon Jan 18 16:22:40 2021
* Arguments: "-B" "macro_default" "-W" "dotnet:Sanwa,SMatLAB,0.0,private" "-T" "link:lib"
* "-d" "D:\SanwaMatLAB\Sanwa\src" "-w" "enable:specified_file_mismatch" "-w"
* "enable:repeated_file" "-w" "enable:switch_ignored" "-w" "enable:missing_lib_sentinel"
* "-w" "enable:demo_license" "-v" "class{SMatLAB:D:\SanwaMatLAB\Sanwa_ployfit.m}" 
*/
using System;
using System.Reflection;
using System.IO;
using MathWorks.MATLAB.NET.Arrays;
using MathWorks.MATLAB.NET.Utility;

#if SHARED
[assembly: System.Reflection.AssemblyKeyFile(@"")]
#endif

namespace SanwaNative
{
  /// <summary>
  /// The SMatLAB class provides a CLS compliant, Object (native) interface to the
  /// M-functions contained in the files:
  /// <newpara></newpara>
  /// D:\SanwaMatLAB\Sanwa_ployfit.m
  /// <newpara></newpara>
  /// deployprint.m
  /// <newpara></newpara>
  /// printdlg.m
  /// </summary>
  /// <remarks>
  /// @Version 0.0
  /// </remarks>
  public class SMatLAB : IDisposable
  {
    #region Constructors

    /// <summary internal= "true">
    /// The static constructor instantiates and initializes the MATLAB Compiler Runtime
    /// instance.
    /// </summary>
    static SMatLAB()
    {
      if (MWMCR.MCRAppInitialized)
      {
        Assembly assembly= Assembly.GetExecutingAssembly();

        string ctfFilePath= assembly.Location;

        int lastDelimiter= ctfFilePath.LastIndexOf(@"\");

        ctfFilePath= ctfFilePath.Remove(lastDelimiter, (ctfFilePath.Length - lastDelimiter));

        string ctfFileName = "Sanwa.ctf";

        Stream embeddedCtfStream = null;

        String[] resourceStrings = assembly.GetManifestResourceNames();

        foreach (String name in resourceStrings)
        {
          if (name.Contains(ctfFileName))
          {
            embeddedCtfStream = assembly.GetManifestResourceStream(name);
            break;
          }
        }
        mcr= new MWMCR("",
                       ctfFilePath, embeddedCtfStream, true);
      }
      else
      {
        throw new ApplicationException("MWArray assembly could not be initialized");
      }
    }


    /// <summary>
    /// Constructs a new instance of the SMatLAB class.
    /// </summary>
    public SMatLAB()
    {
    }


    #endregion Constructors

    #region Finalize

    /// <summary internal= "true">
    /// Class destructor called by the CLR garbage collector.
    /// </summary>
    ~SMatLAB()
    {
      Dispose(false);
    }


    /// <summary>
    /// Frees the native resources associated with this object
    /// </summary>
    public void Dispose()
    {
      Dispose(true);

      GC.SuppressFinalize(this);
    }


    /// <summary internal= "true">
    /// Internal dispose function
    /// </summary>
    protected virtual void Dispose(bool disposing)
    {
      if (!disposed)
      {
        disposed= true;

        if (disposing)
        {
          // Free managed resources;
        }

        // Free native resources
      }
    }


    #endregion Finalize

    #region Methods

    /// <summary>
    /// Provides a single output, 0-input Objectinterface to the Sanwa_ployfit
    /// M-function.
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <returns>An Object containing the first output argument.</returns>
    ///
    public Object Sanwa_ployfit()
    {
      return mcr.EvaluateFunction("Sanwa_ployfit", new Object[]{});
    }


    /// <summary>
    /// Provides a single output, 1-input Objectinterface to the Sanwa_ployfit
    /// M-function.
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <param name="x1">Input argument #1</param>
    /// <returns>An Object containing the first output argument.</returns>
    ///
    public Object Sanwa_ployfit(Object x1)
    {
      return mcr.EvaluateFunction("Sanwa_ployfit", x1);
    }


    /// <summary>
    /// Provides a single output, 2-input Objectinterface to the Sanwa_ployfit
    /// M-function.
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <param name="x1">Input argument #1</param>
    /// <param name="x2">Input argument #2</param>
    /// <returns>An Object containing the first output argument.</returns>
    ///
    public Object Sanwa_ployfit(Object x1, Object x2)
    {
      return mcr.EvaluateFunction("Sanwa_ployfit", x1, x2);
    }


    /// <summary>
    /// Provides the standard 0-input Object interface to the Sanwa_ployfit M-function.
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public Object[] Sanwa_ployfit(int numArgsOut)
    {
      return mcr.EvaluateFunction(numArgsOut, "Sanwa_ployfit", new Object[]{});
    }


    /// <summary>
    /// Provides the standard 1-input Object interface to the Sanwa_ployfit M-function.
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="x1">Input argument #1</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public Object[] Sanwa_ployfit(int numArgsOut, Object x1)
    {
      return mcr.EvaluateFunction(numArgsOut, "Sanwa_ployfit", x1);
    }


    /// <summary>
    /// Provides the standard 2-input Object interface to the Sanwa_ployfit M-function.
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="x1">Input argument #1</param>
    /// <param name="x2">Input argument #2</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public Object[] Sanwa_ployfit(int numArgsOut, Object x1, Object x2)
    {
      return mcr.EvaluateFunction(numArgsOut, "Sanwa_ployfit", x1, x2);
    }


    /// <summary>
    /// This method will cause a MATLAB figure window to behave as a modal dialog box.
    /// The method will not return until all the figure windows associated with this
    /// component have been closed.
    /// </summary>
    /// <remarks>
    /// An application should only call this method when required to keep the
    /// MATLAB figure window from disappearing.  Other techniques, such as calling
    /// Console.ReadLine() from the application should be considered where
    /// possible.</remarks>
    ///
    public void WaitForFiguresToDie()
    {
      mcr.WaitForFiguresToDie();
    }



    #endregion Methods

    #region Class Members

    private static MWMCR mcr= null;

    private bool disposed= false;

    #endregion Class Members
  }
}
