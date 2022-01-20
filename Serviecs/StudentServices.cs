using Demo.DbContextLayer;
using Demo.Describer;
using Demo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Demo.Serviecs
{
    public class StudentServices
    {
        private readonly AppDbContext _context;
        private readonly AppErrorDescriber _describer;

        public StudentServices(AppDbContext context, AppErrorDescriber appErrorDescriber)
        {
            _context = context;
            _describer = appErrorDescriber;

        }

        public async Task<AppResult> Create(Teacher student, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            
            ThrowIfDisposed();
            if (student == null)
            {
                throw new ArgumentNullException("Student");
            }

            try
            {
                await _context.teachers.AddAsync(student);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return AppResult.Failed(_describer.DuplicateStudent());
            }
            catch (Exception)
            {

                throw;
            }

            return AppResult.Success;

        }

        protected void ThrowIfDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
        }

        /// <summary>
        /// Dispose the stores
        /// </summary>
        public void Dispose() => _disposed = true;

        private bool _disposed;



        public class WriteData : IDisposable
        {
          
            // trường lưu trạng thái Dispose
            private bool m_Disposed = false;

            private StreamWriter stream;

            public WriteData(string filename)
            {
                stream = new StreamWriter(filename, true);
            }

            // Phương thức triển khai từ giao diện
            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            protected virtual void Dispose(bool disposing)
            {
                if (!m_Disposed)
                {
                    if (disposing)
                    {
                        // các đối tượng có Dispose gọi ở đây
                        stream.Dispose();
                    }

                    // giải phóng các tài nguyên không quản lý được cửa lớp (unmanaged)

                    m_Disposed = true;
                }
            }

            ~WriteData()
            {
                Dispose(false);
            }

        }
    }
}
