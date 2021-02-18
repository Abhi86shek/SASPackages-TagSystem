﻿using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SAS.TagSystem
{
	[DisallowMultipleComponent()]
	public class Tagger : MonoBehaviour
	{
		[Serializable]
		public class Tag
        {
			[SerializeField] private Component m_Component;
			[SerializeField] private string m_Value;

			public Component Component => m_Component;

            public string Value { get => m_Value; set => m_Value = value; }

			public Tag(Component component, string val)
            {
				m_Component = component;
				m_Value = val;
            }
        }

		public IEnumerable<Component> Find<T>(string tag) where T: Component
		{
			return m_Tags.Where(item => item.Value == tag && item.Component.GetType() == typeof(T)).Select(item => item.Component);
		}

		public IEnumerable<Component> Find(Type type, string tag)
		{
			return m_Tags.Where(item => item.Value == tag && item.Component.GetType() == type).Select(item => item.Component);
		}

		public Tag Find(Component component)
		{
			return m_Tags.FirstOrDefault(tag => tag.Component == component);
		}

		[SerializeField] private List<Tag> m_Tags = new List<Tag>();

		public string GetTag(Component component)
        {
			var tag = m_Tags.Find(ele => ele.Component == component);
			return tag?.Value;
        }

		public void SetTag(Component component, string tagValue = "")
		{
			var tag = m_Tags.Find(ele => ele.Component == component);
			if (tag != null)
				tag.Value = tagValue;
			else
				m_Tags.Add(new Tag(component, tagValue));
		}

		public void RemoveTag(Component component)
		{
			var tag = m_Tags.Find(ele => ele.Component == component);
			m_Tags.Remove(tag);
		}
	}
}