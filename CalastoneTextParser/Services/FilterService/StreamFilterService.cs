using CalastoneTextParser.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalastoneTextParser.Services.FilterService
{
    internal class StreamFilterService : IFilterService, IDisposable
    {
        private readonly StreamReader _stream;
        private readonly int _bufferSize;
        private bool _disposed;

        public StreamFilterService(StreamReader stream)
            : this(stream, 4096)
        {
            _stream = stream;
        }

        public StreamFilterService(StreamReader stream, int bufferSize)
        {
            _stream = stream;
            _bufferSize = bufferSize;
        }

        public IEnumerable<string?> GetNextUnfilteredItem(IEnumerable<ISpanFilter> filters)
        {
            var bufferSize = _bufferSize;
            var buffer = new char[bufferSize];
            int charsInBuffer;
            var startPos = 0;
            while (!_stream.EndOfStream)
            {
                charsInBuffer = TryRead(bufferSize, buffer, startPos);
                var bufferPos = 0;
                while (bufferPos < charsInBuffer)
                {
                    startPos = bufferPos;
                    if (GetItemFromBuffer(buffer, charsInBuffer, ref bufferPos, out ReadOnlySpan<char> item, out bool hitEndOfBuffer))
                    {
                        if (hitEndOfBuffer && !_stream.EndOfStream)
                        {
                            Array.Copy(buffer, startPos, buffer, 0, charsInBuffer - startPos);
                            startPos = charsInBuffer - startPos;
                        }
                        else
                        {
                            // We'll do a memory allocation when we actually want to give someone a string to work with...
                            if (PassesFilter(filters, item))
                                yield return new string(item);
                        }
                    }
                    // Increment passed the separating character
                    bufferPos++;
                }
            }
        }

        private int TryRead(int bufferSize, char[] buffer, int startPos)
        {
            try
            {
                return _stream.Read(buffer, startPos, bufferSize - startPos) + startPos;
            }
            catch (Exception)
            {
                // Log exception, maybe rethrow our own that wraps it.
                return -1;
            }
        }

        private static bool PassesFilter(IEnumerable<ISpanFilter> filters, ReadOnlySpan<char> item)
        {
            foreach (var filter in filters)
            {
                if (filter.ShouldBeFiltered(item))
                    return false;
            }

            return true;
        }

        private bool GetItemFromBuffer(char[] buf, int bufferSize, ref int bufferPos, out ReadOnlySpan<char> item, out bool hitEndOfBuffer)
        {
            item = default;
            hitEndOfBuffer = false;
            if (bufferPos >= bufferSize)
            {
                hitEndOfBuffer = true;
                return false;
            }

            int startPos = bufferPos;
            while (bufferPos < bufferSize && !IsEndOfWordChar(buf[bufferPos]))
            {
                bufferPos++;
            }

            if (bufferPos == startPos)
                return false;
            hitEndOfBuffer = bufferPos == bufferSize;
            item = new(buf, startPos, bufferPos - startPos);
            return true;
        }

        private bool IsEndOfWordChar(char v)
        {
            return !IsLowerCaseChar(v) && !IsUpperCaseChar(v);
        }

        private bool IsUpperCaseChar(char v)
        {
            return v >= 'A' && v <= 'Z';
        }

        private bool IsLowerCaseChar(char v)
        {
            return (v >= 'a' && v <= 'z');
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                _stream.Dispose();
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
