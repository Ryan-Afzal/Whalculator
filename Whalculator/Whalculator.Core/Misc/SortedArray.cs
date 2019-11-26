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

			set {
				this.elements[i] = value;
				int k = i;

				while (k < Length - 1 && this.elements[k].CompareTo(this.elements[k + 1]) > 0) {
					T temp = this.elements[k + 1];
					this.elements[k + 1] = this.elements[k];
					this.elements[k] = temp;
					k++;
				}

				while (k > 0 && this.elements[k].CompareTo(this.elements[k - 1]) < 0) {
					T temp = this.elements[k - 1];
					this.elements[k - 1] = this.elements[k];
					this.elements[k] = temp;
					k--;
				}
			}
		}

		public int Length { 
			get {
				return this.elements.Length;
			}
		}

		public T[] ToArray() {
			T[] output = new T[elements.Length];
			this.elements.CopyTo(output, 0);
			return output;
		}

		IEnumerator<T> IEnumerable<T>.GetEnumerator() {
			return ((IEnumerable<T>)this.elements).GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return ((IEnumerable<T>)this.elements).GetEnumerator();
		}
	}
}
