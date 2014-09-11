namespace Zed.Objects {
    /// <summary>
    /// Abstract class that implements <see cref="IImmutable"/> interface
    /// </summary>
    /// <see href="http://stackoverflow.com/questions/263585/immutable-object-pattern-in-c-what-do-you-think"/>
    /// <see href="http://en.wikipedia.org/wiki/Immutable_object"/>
    /// <see href="http://blogs.msdn.com/b/ericlippert/archive/2007/11/13/immutability-in-c-part-one-kinds-of-immutability.aspx"/>
    public class ImmutableObject : IImmutable {

        #region Members

        /// <summary>
        /// Indication of immutability of current object
        /// </summary>
        public bool IsImmutable { get; private set; }

        #endregion

        #region Constructors and Init

        #endregion

        #region Methods

        /// <summary>
        /// Makes an object immutable
        /// </summary>
        public void Freeze() {
            if (!IsImmutable) {
                IsImmutable = true;
                OnFrozen();
            }
        }

        /// <summary>
        /// The method should be called after 
        /// Metoda koja se poziva poslije proglasenja objekta nepromijenjivim, odnosno nakon poziva metode Freeze() te obavlja odredene poslove.
        /// 
        /// The method is called after the proclamation of the object fixed or after the call method Freeze () and performs certain tasks
        /// </summary>
        protected virtual void OnFrozen() { }

        /// <summary>
        /// Throws an exception if the object is immutable.
        /// The method should be called when we want to make a change on data members. The method throws an exception
        /// if the object is declared immutable, thus preventing changes of data members.
        /// </summary>
        protected void FailIfImmutable() {
            if (IsImmutable) { throw new ImmutableObjectException(this); }
        }


        #endregion

    }
}
