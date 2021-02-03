import {
    Heading,
    Bold,
    Italic,
    Strike,
    Underline,
    Code,
    Paragraph,
    BulletList,
    OrderedList,
    ListItem,
    Link,
    Blockquote,
    HardBreak,
    HorizontalRule,
    History,
} from 'tiptap-vuetify';

export const baseExtensionConfigurations = [
    History,
    Blockquote,
    Link,
    Underline,
    Strike,
    Italic,
    ListItem,
    BulletList,
    OrderedList,
    [
        Heading,
        {
            options: {
                levels: [1, 2, 3],
            },
        },
    ],
    Bold,
    Code,
    HorizontalRule,
    Paragraph,
    HardBreak,
];

export const darkToolbarAttribute = { color: 'grey darken-2' };