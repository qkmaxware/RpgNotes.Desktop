using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RpgNotes.Desktop.Data {

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum FilterOperator {
	And, Or, Xor, 
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum FilterComparison {
	Equal, Contains
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum FilterSelector {
	Tag, Title, Content
}

public class FilterExpression {
	public FilterOperator? Operator {get; set;}
	public FilterComparison? Comparator {get; set;}
	public FilterSelector? Selector {get; set;}
	 
	public List<FilterExpression> SubExpressions {get; set;}
	public string Value {get; set;}

	private bool and(Article article) {
		if (SubExpressions != null && SubExpressions.Count > 0) {
			var match = true;
			foreach (var subexpr in this.SubExpressions) {
				var sub_match = subexpr.IsMatch(article);
				match &= sub_match;
			}
			return match;
		} else {
			return false;
		}
	}
	
	private bool or(Article article) {
		if (SubExpressions != null && SubExpressions.Count > 0) {
			var match = false;
			foreach (var subexpr in this.SubExpressions) {
				var sub_match = subexpr.IsMatch(article);
				match |= sub_match;
			}
			return match;
		} else {
			return false;
		}
	}
	
	private bool xor(Article article) {
		if (SubExpressions != null && SubExpressions.Count > 0) {
			var match = false;
			foreach (var subexpr in this.SubExpressions) {
				var sub_match = subexpr.IsMatch(article);
				if (sub_match) {
					if (match == true) {
						return false; // Cannot match both items
					}
				}
				match |= sub_match;
			}
			return match;
		} else {
			return false;
		}
	}
	
	private bool matches_tag(Article article) {
		return article.Tags().Contains(this.Value);
	}
	private bool matches_title(Article article) {
		return article.Name == this.Value;
	}
	private bool matches_content(Article article) {
		return article.Contents() == this.Value;
	}
	
	private bool test_equal (FilterSelector selector, Article article) {
		return selector switch {
			FilterSelector.Title 	=> matches_title(article),
			FilterSelector.Tag 		=> matches_tag(article),
			FilterSelector.Content	=> matches_content(article),
			_						=> false, 
		};
	}

	private bool contains_tag(Article article) {
		var tags = article.Tags();
		var contains = false;
		foreach (var tag in tags) {
			if (tag.IndexOf(this.Value, StringComparison.OrdinalIgnoreCase) >= 0) {
				contains = true;
				break;
			} else {
				continue;
			}
		}
		return contains;
	}
	private bool contains_title(Article article) {
		return article.Name.IndexOf(this.Value, StringComparison.OrdinalIgnoreCase) >= 0;
	}
	private bool contains_content(Article article) {
		return article.Contents().IndexOf(this.Value, StringComparison.OrdinalIgnoreCase) >= 0;
	}

	private bool test_contains (FilterSelector selector, Article article) {
		return selector switch {
			FilterSelector.Title 	=> contains_title(article),
			FilterSelector.Tag 		=> contains_tag(article),
			FilterSelector.Content	=> contains_content(article),
			_						=> false, 
		};
	}

	public bool IsMatch(Article article) {
		if (Operator.HasValue) {
			return Operator.Value switch {
				FilterOperator.And		=> and(article),
				FilterOperator.Or		=> or(article),
				FilterOperator.Xor 		=> xor(article),
				_ 						=> false
			};
		} else if (Selector.HasValue && Comparator.HasValue) {
			var compareTo = this.Value;
			return Comparator.Value switch {
				FilterComparison.Equal	=> test_equal(this.Selector.Value, article),
				FilterComparison.Contains=>test_contains(this.Selector.Value, article),
				_						=> false
			};
		} else {
			return false;
		}
		
	}

	/*
	Filter Language
	expr := (check) (('or' | 'xor' | 'and') (check))*
	check := key comparison value
	key := 'tag' | 'title' | 'content'
	comparison := '=', 'contains'
	value := \w+
	*/

	public static bool TryCompile(string filter, out FilterExpression expression, out string error) {
		var queue = new Queue<FilterExpression>();
		string[] parts = filter.Split(' ', StringSplitOptions.RemoveEmptyEntries);
		try {
			expression = compileExpression(parts);
			error = null;
			return true;
		} catch (Exception e) {
			expression = null;
			error = e.Message;
			return false;
		}
	}
	private static string inRange(int index, string[] parts) {
		if (index >= 0 && index < parts.Length) {
			return parts[index];
		} else {
			throw new ArgumentException("Missing required argument");
		}
	}
	private static bool isNext(int index, string[] parts, string value) {
		if (index >= 0 && index < parts.Length) {
			return parts[index] == value;
		} else {
			return false;
		}
	}
	private static bool isOneOf(int index, string[] parts, string[] values) {
		if (index >= 0 && index < parts.Length) {
			return values.Contains(parts[index]);
		} else {
			return false;
		}
	}
	private static FilterExpression compileExpression(string[] parts) {
		var index = 0;
		var first = compileCheck(ref index, parts);
		while(isOneOf(index, parts, new string[]{"and", "or", "xor"})) {
			var op = inRange(index, parts); index++;
			var next = compileCheck(ref index, parts);
			if (op == "and") {
				first = new FilterExpression {
					Operator = FilterOperator.And,
					SubExpressions = new List<FilterExpression> { first, next }
				};
			} else if (op == "or") {
				first = new FilterExpression {
					Operator = FilterOperator.Or,
					SubExpressions = new List<FilterExpression> { first, next }
				};
			}
			else if (op == "xor") {
				first = new FilterExpression {
					Operator = FilterOperator.Xor,
					SubExpressions = new List<FilterExpression> { first, next }
				};
			}
		}
		return first;
	}
	private static FilterExpression compileCheck(ref int index, string[] parts) {
		var key = compileKey(ref index, parts);
		var comparison = compileComparison(ref index, parts);
		var value = inRange(index, parts); index++;
		return new FilterExpression {
			Selector 	= key,
			Comparator 	= comparison,
			Value 		= value
		};
	}
	private static FilterSelector compileKey(ref int index, string[] parts) {
		var element = inRange(index, parts);
		if (element == "tag") {
			index++;
			return FilterSelector.Tag;
		} else if (element == "title") {
			index++;
			return FilterSelector.Title;
		} else if (element == "content") {
			index++;
			return FilterSelector.Content;
		} else {
			throw new ArgumentException("Missing selector");
		}
	}
	private static FilterComparison compileComparison(ref int index, string[] parts) {
		var element = inRange(index, parts);
		if (element == "=") {
			index++;
			return FilterComparison.Equal;
		} else if (element == "contains") {
			index++;
			return FilterComparison.Contains;
		} else {
			throw new ArgumentException("Missing comparison operator");
		}
	}
}

public class NodeGroup {
	public string Title {get; set;}
	public string Colour {get; set;}
	public FilterExpression Filter {get; set;}

	private string filterString;
	public string CompilationErrorMessage;
	[JsonIgnore]
	public bool CompiledFromString {get; private set;} = false;
	[JsonIgnore]
	public string FilterString {
		get => filterString;
		set {
			this.filterString = value;
			// Try to compile filter
			FilterExpression expr = null;
			this.CompiledFromString = FilterExpression.TryCompile(value, out expr, out CompilationErrorMessage);
			this.Filter = expr;
		}
	}
	public bool IsFilterStringValid() {
		if (string.IsNullOrEmpty(this.filterString))
			return false;
		return true;
	}
	
	public bool IsMatch(Article article) {
		if (Filter == null)
			return false;
		else 
			return Filter.IsMatch(article);
	}
}

}