namespace MemeVM.Runtime.Engine
{
    internal enum VMState
    {
        Next,
        Exception,
        Rethrow,
        Return
    }
}