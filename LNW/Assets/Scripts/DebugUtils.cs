#define DEBUG

using UnityEngine;
using System.Collections;
using System;

public static class DebugUtils
{ 
		public static void Assert (bool condition)
		{ 
				#if DEBUG 
				if (!condition)
						throw new Exception (); 
				#endif 
		} 
}