/* 
Copyright (c) 2009-2010 Konnetic Ltd.
*/
/* Change history
* 20 Jan 2010  James Wright james@konnetic.com Baseline Implementation
*/

using System;
using System.Collections;
using System.Collections.Generic;

namespace Konnetic.Sip.Headers
	{
	/// <summary>
    /// Implements the Iterator pattern for the <see cref="T:Konnetic.Sip.Headers.SipParameterCollection"/>.
	/// </summary>
    public class SipParameterCollectionEnumerator : IEnumerator<SipParameter>
		{
		private int _position;
		private SipParameterCollection _collection;

		/// <summary>
		/// Gets or sets the position.
		/// </summary>
		/// <value>The position.</value>
		public int Position
			{
			get { return _position; }
            set
                {
                if(value >= 0 && value < (this.Parameters.Count-1))
                    {
                    _position = value;
                    }
                }
			}

		/// <summary>
		/// Gets or sets the parameters.
		/// </summary>
		/// <value>The parameters.</value>
		public SipParameterCollection Parameters
			{
			get { return _collection; }
			set { _collection = value; }
			}

		/// <summary>
		/// Initializes a new instance of the <see cref="SipParameterCollectionEnumerator"/> class.
		/// </summary>
		/// <remarks>The default constructor.</remarks>
		/// <overloads>
		/// <summary>The method has one overload.</summary>
		/// </overloads>
		public SipParameterCollectionEnumerator()
			{
			_position = -1;
			_collection = new SipParameterCollection();
			}

		/// <summary>
		/// Initializes a new instance of the <see cref="SipParameterCollectionEnumerator"/> class.
		/// </summary>
		/// <param name="parameters">The parameters.</param>
		public SipParameterCollectionEnumerator(SipParameterCollection parameters)
			{
			_position = -1;
			_collection = parameters;
			}

		#region IEnumerator Members
		/// <summary>
		/// Gets the current element in the collection.
		/// </summary>
		/// <value></value>
		/// <returns>
		/// The current element in the collection.
		/// </returns>
		/// <exception cref="T:System.InvalidOperationException">
		/// The enumerator is positioned before the first element of the collection or after the last element.
		/// </exception>
		public virtual SipParameter Current
			{
			get { return _collection[_position]; }
			}
 

		/// <summary>
		/// Advances the enumerator to the next element of the collection.
		/// </summary>
		/// <returns>
		/// true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.
		/// </returns>
		/// <exception cref="T:System.InvalidOperationException">
		/// The collection was modified after the enumerator was created.
		/// </exception>
		[return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U1)]
		public bool MoveNext()
			{
			if(_position < _collection.Count - 1)
				{
				_position++;
				return true;
				}
			else
				{
				return false;
				}
			}

		/// <summary>
		/// Sets the enumerator to its initial position, which is before the first element in the collection.
		/// </summary>
		/// <exception cref="T:System.InvalidOperationException">
		/// The collection was modified after the enumerator was created.
		/// </exception>
		public void Reset()
			{
			_position = -1;
			}

		#endregion

        public void Dispose()
            { 
            }
         

        void IDisposable.Dispose()
            { 
            }


        object IEnumerator.Current
            {
            get { return Current; }
            }
        }
	}
