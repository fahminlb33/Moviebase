using System.Collections.Generic;
using System.Linq;
using System.Text;

// ReSharper disable UnusedParameter.Local
// ReSharper disable once CheckNamespace
namespace System.IO
{
    public sealed class PowerPath : ICloneable
    {
        private string _root;
        private readonly List<string> _dirLevels;
        private string _filename;
        private string _extension;

        #region Properties

        public bool IsFile => _filename != null;

        public bool IsDirectory => !IsFile;

        #endregion

        #region Constructors
        public PowerPath(string basePath) : this(basePath, !string.IsNullOrWhiteSpace(Path.GetExtension(basePath)))
        {
        }

        public PowerPath(string basePath, bool isFile)
        {
            ArgumentContract.NotNull(basePath);
            ArgumentContract.ValidatePathName(basePath);

            _dirLevels = new List<string>();
            _root = Path.GetPathRoot(basePath);
            if (isFile)
            {
                _filename = Path.GetFileNameWithoutExtension(basePath);
                _extension = Path.GetExtension(basePath);
            }

            var levels = basePath.Split(Path.DirectorySeparatorChar);
            for (int i = 1; i < levels.Length; i++)
            {
                if (i == levels.Length - 1 && isFile) continue;
                _dirLevels.Add(levels[i]);
            }
        }
        #endregion

        #region Methods

        #region Directory Methods

        /// <summary>
        /// Sets current path root.
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public PowerPath SetPathRoot(string root)
        {
            ArgumentContract.NotNull(root);
            ArgumentContract.IsLength(root, 2);
            if (!root.EndsWith(":"))
                throw new ArgumentException("Path root doesn't ends with semicolon.");
            if (char.IsLetter(root[0]))
                throw new ArgumentException("Path root doesn't starts with letter.");

            _root = root;
            return this;
        }

        /// <summary>
        /// Add one level to current path.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public PowerPath AddLevel(string name)
        {
            ArgumentContract.NotNull(name);
            ArgumentContract.ValidateFileName(name);

            _dirLevels.Add(name);
            return this;
        }

        /// <summary>
        /// Remove one last level from current path.
        /// </summary>
        /// <returns></returns>
        public PowerPath UpOneLevel()
        {
            if (_dirLevels.Count > 1)
                _dirLevels.RemoveAt(_dirLevels.Count - 1);
            return this;
        }

        /// <summary>
        /// Rename last directroy name to new name.
        /// </summary>
        /// <param name="toName"></param>
        /// <returns></returns>
        public PowerPath RenameLastDirectory(string toName)
        {
            ArgumentContract.NotNull(toName);
            ArgumentContract.ValidateFileName(toName);

            _dirLevels[_dirLevels.Count - 1] = toName;
            return this;
        }

        /// <summary>
        /// Rename last directory name to new name using specified pattern.
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public PowerPath RenameLastDirectoryByPattern(string pattern, object values)
        {
            ArgumentContract.NotNull(pattern);
            ArgumentContract.NotNull(values);
            
            _dirLevels[_dirLevels.Count - 1] = InternalPatternRename(pattern, values, true); 
            return this;
        }

        /// <summary>
        /// Swaps the directory name from beginning to end of directory name.
        /// </summary>
        /// <param name="startsWith"></param>
        /// <returns></returns>
        public PowerPath SwapLastDirectoryName(string startsWith)
        {
            ArgumentContract.NotNull(startsWith);
            var dirName = GetLastDirectoryName();
            var result = InternalSwapName(dirName, startsWith);
            if (result != null) RenameLastDirectory(result);

            return this;
        }

        #endregion

        #region File Methods

        /// <summary>
        /// Change current file extension to new extension.
        /// </summary>
        /// <param name="toExtension"></param>
        /// <returns></returns>
        public PowerPath ChangeExtension(string toExtension)
        {
            ArgumentContract.NotNull(toExtension);
            
            if (_extension == null) return this;
            if (!toExtension.StartsWith(".")) toExtension = "." + toExtension;
            _extension = toExtension;
            return this;
        }

        /// <summary>
        /// Rename file to new name.
        /// </summary>
        /// <param name="toName"></param>
        /// <returns></returns>
        public PowerPath RenameFile(string toName)
        {
            ArgumentContract.NotNull(toName);
            ArgumentContract.ValidateFileName(toName);

            if (_filename != null) _filename = toName;
            return this;
        }

        /// <summary>
        /// Rename file to new name using specified pattern.
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="values"></param>
        /// <param name="autoStripChars"></param>
        /// <returns></returns>
        public PowerPath RenameFileByPattern(string pattern, object values, bool autoStripChars = true)
        {
            ArgumentContract.NotNull(pattern);
            ArgumentContract.NotNull(values);

            _filename = InternalPatternRename(pattern, values, autoStripChars);
            return this;
        }

        /// <summary>
        /// Swaps the file name from beginning to end of file name.
        /// </summary>
        /// <param name="startsWith"></param>
        /// <returns></returns>
        public PowerPath SwapFileName(string startsWith)
        {
            ArgumentContract.NotNull(startsWith);
            var result = InternalSwapName(_filename, startsWith);
            if (result != null) _filename = result;

            return this;
        }

        #endregion

        #region Common Methods

        public static bool IsPathNameValid(string input)
        {
            return !Path.GetInvalidPathChars().Any(input.Contains);
        }

        public static bool IsFileNameValid(string input)
        {
            return !Path.GetInvalidFileNameChars().Any(input.Contains);
        }

        public string GetFileNameWithoutExtension()
        {
            return _filename;
        }

        public string GetFileName()
        {
            return _filename + _extension;
        }

        public string GetDirectoryPath()
        {
            return Path.Combine(_root, string.Join("\\", _dirLevels));
        }

        public string GetLastDirectoryName()
        {
            return _dirLevels[_dirLevels.Count - 1];
        }

        public string GetFullPath()
        {
            var combinedPath = GetDirectoryPath();
            if (_filename == null) return combinedPath;

            combinedPath = Path.Combine(combinedPath, _filename);
            if (_extension != null) combinedPath += _extension;
            return combinedPath;
        }

        public string GetExtension()
        {
            return _extension;
        }

        #endregion

        #region Internals

        private string InternalSwapName(string input, string swapText)
        {
            if (!input.Contains(swapText)) return null;

            var removed = input.Replace(swapText, "");
            return removed.Trim() + ", " + swapText;
        }

        private string InternalPatternRename(string pattern, object values, bool autoStrip)
        {
            var sb = new StringBuilder(pattern);
            foreach (var prop in values.GetType().GetProperties())
            {
                if (!pattern.Contains(prop.Name)) continue;
                sb.Replace("{" + prop.Name + "}", prop.GetValue(values, null).ToString());
            }

            if (autoStrip)
            {
                foreach (var invalidFileNameChar in Path.GetInvalidFileNameChars())
                {
                    sb.Replace(invalidFileNameChar.ToString(), "");
                }
            }

            var temp = sb.ToString();
            if (!IsFileNameValid(temp) && !autoStrip)
                throw new FormatException("Resulting path name is invalid. Change your pattern.");
            
            return temp;
        }
        
        #endregion 

        #endregion

        #region Overrides

        public object Clone()
        {
            return new PowerPath(GetFullPath());
        }

        public override string ToString()
        {
            return GetFullPath();
        }

        public static implicit operator string(PowerPath d)
        {
            return d.GetFullPath();
        } 
        
        #endregion

        public void Clear()
        {
            _dirLevels.Clear();
            _root = null;
            _extension = null;
            _filename = null;
        }

        private static class ArgumentContract
        {
            public static void NotNull(object value)
            {
                if (value == null) throw new ArgumentNullException();
                if (value is string && string.IsNullOrWhiteSpace(value.ToString()))
                    throw new ArgumentException("String argument is null or whitespace.");
            }

            public static void ValidatePathName(string name)
            {
                if (!IsPathNameValid(name))
                    throw new FormatException("Specified path name contains invalid char(s).");
            }

            public static void ValidateFileName(string name)
            {
                if (!IsFileNameValid(name))
                    throw new FormatException("Specified file name contains invalid char(s).");
            }

            public static void IsLength(string input, int count)
            {
                if (input.Length != count)
                    throw new ArgumentException($"Specified string argument is not {count}");
            }
        }
    }
}
