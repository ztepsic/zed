namespace Zed.Objects {
    /// <summary>
    /// Interface that defines immutable object (Popsicle immutability)
    /// </summary>
    /// <see href="http://stackoverflow.com/questions/263585/immutable-object-pattern-in-c-what-do-you-think"/>
    /// <see href="http://en.wikipedia.org/wiki/Immutable_object"/>
    /// <see href="http://blogs.msdn.com/b/ericlippert/archive/2007/11/13/immutability-in-c-part-one-kinds-of-immutability.aspx"/>
    public interface IImmutable {
        /// <summary>
        /// Indication of immutability of current object
        /// </summary>
        bool IsImmutable { get; }

        /// <summary>
        /// Makes an object immutable
        /// </summary>
        void Freeze();
    }
}
