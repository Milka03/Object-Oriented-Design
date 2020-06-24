using Problems;

namespace Solvers
{
    interface ISolver
    {
        void Accept(Problem visitor);
    }
}