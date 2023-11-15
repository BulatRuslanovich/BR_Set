using System.Collections;
using System.Collections.Generic;

namespace MySet {
    public class MyEasySet<T> : IEnumerable {
        private List<T> items = new List<T>();

        public int Count => items.Count;
        public MyEasySet() {}

        public MyEasySet(T item) {
            items.Add(item);
        }

        public MyEasySet(IEnumerable<T> items) {
            this.items = items.ToList();
        }

        public void Add(T item) {
            if (items.Contains(item)) {
                return;
            }

            items.Add(item);
        }

        public void Remove(T item) {
            items.Remove(item);
        }

        public MyEasySet<T> Union(MyEasySet<T> set) {
            //! with using Linq
            // return new MyEasySet<T>(items.Union(set.items));

            MyEasySet<T> result = new MyEasySet<T>();

            foreach (var item in items) {
                result.Add(item);
            }

            foreach (var item in set.items) {
                result.Add(item);
            }

            return result;
        }

        public MyEasySet<T> Intersection(MyEasySet<T> set) {
            //! with using Linq
            // return new MyEasySet<T>(items.Intersect(set.items));

            var result = new MyEasySet<T>();
            MyEasySet<T> big;
            MyEasySet<T> small;

            if (Count > set.Count) {
                big = this;
                small = set;
            } else {
                big = set;
                small = this;
            }

            //? big and small for optimize the method
            foreach (var item1 in small.items) {
                foreach (var item2 in big.items) {
                    if (item1.Equals(item2)) {
                        result.Add(item1);
                        break;
                    }
                }
            }

            return result;
        }

        public MyEasySet<T> Difference(MyEasySet<T> set) {
            //! with using Linq
            // return new MyEasySet<T>(items.Except(set.items));

            var result = new MyEasySet<T>(items);

            foreach (var item in set.items) {
                result.Remove(item);
            }

            return result;
        }

        public bool Subset(MyEasySet<T> set) {
            //! with using Linq
            // return items.items.All(i => set.Contains(i));

            foreach (var item1 in items) {
                var equals = false;
                foreach (var item2 in set.items) {
                    if (item1.Equals(item2)) {
                        equals = true;
                        break;
                    }
                }

                if (!equals) {
                    return false;
                }
            }

            return true;
        }

        public MyEasySet<T> SymmetricDifference(MyEasySet<T> set) {
            //! with using Linq
            // return new MyEasySet<T>(items.Except(set,items).Union(set.items.Except(items)));

            var result = new MyEasySet<T>();

            foreach (var item1 in items) {
                var equals = true;
                foreach (var item2 in set.items) {
                    if (item1.Equals(item2)) {
                        equals = false;
                        break;
                    }
                }

                if (equals) {
                    result.Add(item1);
                }
            }

            foreach (var item1 in set.items) {
                var equals = true;
                foreach (var item2 in items) {
                    if (item1.Equals(item2)) {
                        equals = false;
                        break;
                    }
                }

                if (equals) {
                    result.Add(item1);
                }
            }

            return result;
        }

        public IEnumerator GetEnumerator() {
            return items.GetEnumerator();
        }
    }
}