using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RpgNotes.Desktop.Data {

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum FilterSelector {
	And, Or, Xor, Tag, Title, 
}

public class FilterExpression {
	public FilterSelector Selector {get; set;}
	public FilterExpression[] SubExpressions {get; set;}
	public string Value {get; set;}
	
	private bool and(Article article) {
		if (SubExpressions != null && SubExpressions.Length > 0) {
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
		if (SubExpressions != null && SubExpressions.Length > 0) {
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
		if (SubExpressions != null && SubExpressions.Length > 0) {
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
	
	public bool IsMatch(Article article) {
		return Selector switch {
			FilterSelector.And	=> and(article),
			FilterSelector.Or	=> or(article),
			FilterSelector.Xor 	=> xor(article),
			FilterSelector.Tag 	=> matches_tag(article),
			FilterSelector.Title=> matches_title(article),
			_ 				=> false
		};
	}
}

public class NodeGroup {
	public string Title {get; set;}
	public string Colour {get; set;}
	public FilterExpression Filter {get; set;}
	
	public bool IsMatch(Article article) {
		if (Filter == null)
			return false;
		else 
			return Filter.IsMatch(article);
	}
}

}