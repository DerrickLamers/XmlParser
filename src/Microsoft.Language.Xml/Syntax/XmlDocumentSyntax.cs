﻿using System;
using System.Collections.Generic;

namespace Microsoft.Language.Xml
{
    using InternalSyntax;

    public class XmlDocumentSyntax : XmlNodeSyntax
    {
        internal new class Green : XmlNodeSyntax.Green
        {
            readonly XmlDeclarationSyntax.Green prologue;
            readonly GreenNode precedingMisc;
            readonly XmlNodeSyntax.Green body;
            readonly GreenNode followingMisc;
            readonly SyntaxToken.Green eof;

            internal XmlDeclarationSyntax.Green Prologue => prologue;
            internal GreenNode PrecedingMisc => precedingMisc;
            internal XmlNodeSyntax.Green Body => body;
            internal GreenNode FollowingMisc => followingMisc;
            internal SyntaxToken.Green Eof => eof;

            internal Green(XmlDeclarationSyntax.Green prologue, GreenNode precedingMisc, XmlNodeSyntax.Green body, GreenNode followingMisc, SyntaxToken.Green eof)
                : base(SyntaxKind.XmlDocument)
            {
                this.SlotCount = 5;
                this.prologue = prologue;
                AdjustWidth(prologue);
                this.precedingMisc = precedingMisc;
                AdjustWidth(precedingMisc);
                this.body = body;
                AdjustWidth(body);
                this.followingMisc = followingMisc;
                AdjustWidth(followingMisc);
                this.eof = eof;
                AdjustWidth(eof);
            }

            internal override SyntaxNode CreateRed(SyntaxNode parent, int position) => new XmlDocumentSyntax(this, parent, position);

            internal override GreenNode GetSlot(int index)
            {
                switch (index)
                {
                    case 0: return prologue;
                    case 1: return precedingMisc;
                    case 2: return body;
                    case 3: return followingMisc;
                    case 4: return eof;
                }
                throw new InvalidOperationException();
            }

            internal override GreenNode Accept(InternalSyntax.SyntaxVisitor visitor)
            {
                return visitor.VisitXmlDocument(this);
            }
        }

        internal new Green GreenNode => (Green)base.GreenNode;

        XmlDeclarationSyntax prologue;
        SyntaxNode precedingMisc;
        XmlNodeSyntax body;
        SyntaxNode followingMisc;
        SyntaxToken eof;

        public XmlDeclarationSyntax Prologue => GetRed(ref prologue, 0);
        public SyntaxList<SyntaxNode> PrecedingMisc => new SyntaxList<SyntaxNode>(GetRed(ref precedingMisc, 1));
        public XmlNodeSyntax Body => GetRed(ref body, 2);
        public SyntaxList<SyntaxNode> FollowingMisc => new SyntaxList<SyntaxNode>(GetRed(ref followingMisc, 3));
        public SyntaxToken Eof => GetRed(ref eof, 4);

        internal XmlDocumentSyntax(Green green, SyntaxNode parent, int position)
            : base(green, parent, position)
        {

        }

        public override SyntaxNode Accept(SyntaxVisitor visitor)
        {
            return visitor.VisitXmlDocument(this);
        }

        internal override SyntaxNode GetCachedSlot(int index)
        {
            switch (index)
            {
                case 0: return prologue;
                case 1: return precedingMisc;
                case 2: return body;
                case 3: return followingMisc;
                case 4: return eof;
                default: return null;
            }
        }

        internal override SyntaxNode GetNodeSlot(int slot)
        {
            switch (slot)
            {
                case 0: return Prologue;
                case 1: return GetRed(ref precedingMisc, 1);
                case 2: return Body;
                case 3: return GetRed(ref followingMisc, 3);
                case 4: return Eof;
                default: return null;
            }
        }

        public IXmlElementSyntax RootSyntax => Body as IXmlElementSyntax;
        public IXmlElement Root => RootSyntax.AsElement;
    }
}
