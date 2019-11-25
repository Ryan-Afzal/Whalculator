using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Whalculator.Core.Misc {
	public class SortedArray<T> : IEnumerable<T> where T : IComparable<T> {

		private readonly T[] elements;

		public SortedArray(params T[] elements) {
			this.elements = new T[elements.Length];
			elements.CopyTo(this.elements, 0);
			Array.Sort(this.elements);
		}

		public T this[int i] {
			get {
				return this.elements[i];
			}

			set {//Set BUT maintain sorted order, that is the index will be set but then it will be sorted into place.
				throw new NotImplementedException();
			}
		}

		public int Length { 
			get {
				return this.elements.Length;
			}
		}

		IEnumerator<T> IEnumerable<T>.GetEnumerator() {
			return ((IEnumerable<T>)this.elements).GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return ((IEnumerable<T>)this.elements).GetEnumerator();
		}
	}
}
