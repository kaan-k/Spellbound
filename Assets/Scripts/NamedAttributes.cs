using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NamedAttributes : PropertyAttribute
{
    public readonly string[] names;
    public NamedAttributes(string[] names) {  this.names = names; }
}
