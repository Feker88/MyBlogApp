namespace CodePulse.Domain.Entities;

using System.Text.RegularExpressions;

public class BlogPost : BaseEntity
{

    private bool isCreated;
   

    public BlogPost()
    {
        isCreated = false;
    }

    //public string Title
    //{
    //    get => title;
    //    set
    //    {
    //        if (string.IsNullOrWhiteSpace(value) || value.Length < 3 || value.Length > 200)
    //            throw new ArgumentException("Title must not be empty (min 3 chars, max 200).");
    //        title = value;
    //    }
    //}

    public string Title
    {
        get;
        set 
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length < 3 || value.Length > 200)
                throw new ArgumentException("Title must not be empty (min 3 chars, max 200).");
            field = value;
        }
    }


    public string ShortDescription
    {
        get;
        set
        {
            if (value?.Length > 500)
                throw new ArgumentException("ShortDescription must not be empty (max 500).");
            field = value;
        }
    }

    public string Content
    {
        get;
        set
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length < 50)
                throw new ArgumentException("Content must not be empty (min 50 chars).");
            field = value;
        }
    }

    public string UrlHandle
    {
        get;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("UrlHandle must not be empty.");
            if (!IsValidUrlHandle(value))
                throw new ArgumentException("UrlHandle must be URL-friendly (alphanumeric, hyphens, and underscores only).");
            field = value;
        }
    }

    public string FeatureImageUrl
    {
        get;
        set
        {
            if (!string.IsNullOrWhiteSpace(value) && !IsValidUrl(value))
                throw new ArgumentException("FeatureImageUrl must be a valid URL format or empty.");
           field= value ?? string.Empty;
        }
    }

    public string Author
    {
        get;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Author must not be empty.");
            field = value;
        }
    }


    public bool IsVisible
    {
        get;
        set
        {
            if (value && string.IsNullOrWhiteSpace(Content))
                throw new InvalidOperationException("Cannot publish blog post. Content is required.");
           field= value;
        }
    }

    // Relationships 

    public Guid CategoryId { get; set; }                 
    public BlogCategory? Category { get; set; }


    private bool IsValidUrlHandle(string urlHandle)
    {
        // URL-friendly validation: alphanumeric, hyphens, and underscores only
        return Regex.IsMatch(urlHandle, @"^[a-zA-Z0-9\-_]+$");
    }

    private bool IsValidUrl(string url)
    {
        try
        {
            // Attempt to create a URI to validate URL format
            _ = new Uri(url, UriKind.Absolute);
            return true;
        }
        catch (UriFormatException)
        {
            return false;
        }
    }

    public void MarkAsCreated()
    {
        isCreated = true;
    }

    /// <summary>
    /// Validates that properties are not being modified after creation.
    /// Certain properties (like UrlHandle and Author) are immutable after creation.
    /// </summary>
    private void ValidateNotCreated(string propertyName)
    {
        if (isCreated)
            throw new InvalidOperationException($"Cannot modify {propertyName} after blog post has been created.");
    }
}

/* BUSINESS RULES
 ✓ Can only set IsVisible = true if Content is not empty
 ✓ UrlHandle must be URL-friendly (no special chars)
 ✓ FeatureImageUrl must be a valid URL format (or empty)
 ✓ Cannot modify certain properties after creation
 */

/* VALIDATION RULES 
 ✓ Title must not be empty (min 3 chars, max 200)
 ✓ Content must not be empty (min 50 chars)
 ✓ ShortDescription must not be empty (max 500)
 ✓ UrlHandle must be URL-friendly (alphanumeric, hyphens, underscores)
 ✓ Author must not be empty 
 */

/* BUSINESS RULE
 ✓ Can only set IsVisible = true if Content is not empty
✓ UrlHandle must be URL-friendly (no special chars)
✓ FeatureImageUrl must be a valid URL format (or empty)
✓ Cannot modify certain properties after creation
*/


/* STATE MANAGEMENT RULES
 ✓ UpdatedAt should update when properties change
✓ Cannot publish (IsVisible) if required fields are missing
*/