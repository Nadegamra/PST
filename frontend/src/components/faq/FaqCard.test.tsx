/**
 * @jest-environment jsdom
 */
import React from 'react';
import { render } from '@testing-library/react';
import FaqCard from './FaqCard'; 
import '@testing-library/jest-dom/extend-expect';

describe('FaqCard', () => {
    it('renders with a rounded top appearance when pos is top', () => {
        const { container } = render(
            <FaqCard pos="top" buttonText="Test Button" content="Test Content" id={1} />
        );

        const faqCardElement = container?.firstChild?.firstChild;
        expect(faqCardElement).toHaveClass('rounded-t-lg');
    });

    it('renders without any rounded edges when pos is middle', () => {
        const { container } = render(
            <FaqCard pos="middle" buttonText="Test Button" content="Test Content" id={1} />
        );

        const faqCardElement = container?.firstChild?.firstChild;
        expect(faqCardElement).not.toHaveClass('rounded-t-lg');
        expect(faqCardElement).not.toHaveClass('rounded-b-lg');
    });

    it('renders with a rounded bottom appearance when pos is bottom', () => {
        const { container } = render(
            <FaqCard pos="bottom" buttonText="Test Button" content="Test Content" id={1} />
        );

        const faqCardElement = container?.firstChild?.firstChild;
        expect(faqCardElement).toHaveClass('rounded-b-lg');
    });
});
