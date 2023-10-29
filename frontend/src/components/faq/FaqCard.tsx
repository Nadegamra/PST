interface Props {
    pos: 'top' | 'middle' | 'bottom'
    buttonText: string
    content: string
    id: number
}

export default function FaqCard({ pos, buttonText, content, id }: Props) {
    return (
        <div>
            {pos === 'top' ? (
                <div className="rounded-t-lg mx-5 bg-bg-secondary border-b">
                    <FaqCardButton buttonText={buttonText} id={id} pos={pos} />
                    <FaqCardContent content={content} id={id} pos={pos} />
                </div>
            ) : pos === 'bottom' ? (
                <div className="rounded-b-lg mx-5 bg-bg-secondary">
                    <FaqCardButton buttonText={buttonText} id={id} pos={pos} />
                    <FaqCardContent content={content} id={id} pos={pos} />
                </div>
            ) : (
                <div className="mx-5 bg-bg-secondary border-b">
                    <FaqCardButton buttonText={buttonText} id={id} pos={pos} />
                    <FaqCardContent content={content} id={id} pos={pos} />
                </div>
            )}
        </div>
    )
}

function FaqCardButton({
    buttonText,
    id,
    pos
}: {
    buttonText: string
    id: number
    pos: 'top' | 'middle' | 'bottom'
}) {
    return (
        <h2 className="mb-0" id={'heading' + id}>
            {pos === 'top' ? (
                <button
                    className="group text-fs-h2 font-bold flex w-full items-center rounded-t-[15px] border-0 py-4 px-5 text-left text-base text-neutral-800 transition [overflow-anchor:none] hover:z-[2] focus:z-[3] focus:outline-none [&:not([data-te-collapse-collapsed])]:text-t-primary dark:[&:not([data-te-collapse-collapsed])]:text-bg-extra [&:not([data-te-collapse-collapsed])]:[box-shadow:inset_0_-1px_0_rgba(75,85,99)]"
                    type="button"
                    data-te-collapse-init
                    data-te-target={'#collapse' + id}
                    aria-expanded="true"
                    aria-controls={'collapse' + id}>
                    {buttonText}
                    <span className="ml-auto -mr-1 h-5 w-5 shrink-0 rotate-[-180deg] fill-bg-extra transition-transform duration-200 ease-in-out group-[[data-te-collapse-collapsed]]:mr-0 group-[[data-te-collapse-collapsed]]:rotate-0 group-[[data-te-collapse-collapsed]]:fill-bg-extra motion-reduce:transition-none">
                        <svg
                            xmlns="http://www.w3.org/2000/svg"
                            fill="none"
                            viewBox="0 0 24 24"
                            strokeWidth="1.5"
                            stroke="currentColor"
                            className="h-6 w-6">
                            <path
                                stroke-linecap="round"
                                stroke-linejoin="round"
                                d="M19.5 8.25l-7.5 7.5-7.5-7.5"
                            />
                        </svg>
                    </span>
                </button>
            ) : (
                <button
                    className="group text-fs-h2 font-bold flex w-full items-center rounded-t-[15px] border-0 py-4 px-5 text-left text-base text-neutral-800 transition [overflow-anchor:none] hover:z-[2] focus:z-[3] focus:outline-none [&:not([data-te-collapse-collapsed])]:text-t-primary dark:[&:not([data-te-collapse-collapsed])]:text-bg-extra [&:not([data-te-collapse-collapsed])]:[box-shadow:inset_0_-1px_0_rgba(75,85,99)]"
                    type="button"
                    data-te-collapse-init
                    data-te-collapse-collapsed
                    data-te-target={'#collapse' + id}
                    aria-expanded="false"
                    aria-controls={'collapse' + id}>
                    {buttonText}
                    <span className="ml-auto -mr-1 h-5 w-5 shrink-0 rotate-[-180deg] fill-bg-extra transition-transform duration-200 ease-in-out group-[[data-te-collapse-collapsed]]:mr-0 group-[[data-te-collapse-collapsed]]:rotate-0 group-[[data-te-collapse-collapsed]]:fill-bg-extra motion-reduce:transition-none">
                        <svg
                            xmlns="http://www.w3.org/2000/svg"
                            fill="none"
                            viewBox="0 0 24 24"
                            stroke-width="1.5"
                            stroke="currentColor"
                            className="h-6 w-6">
                            <path
                                stroke-linecap="round"
                                stroke-linejoin="round"
                                d="M19.5 8.25l-7.5 7.5-7.5-7.5"
                            />
                        </svg>
                    </span>
                </button>
            )}
        </h2>
    )
}

function FaqCardContent({
    content,
    id,
    pos
}: {
    content: string
    id: number
    pos: 'top' | 'middle' | 'bottom'
}) {
    return (
        <div>
            {pos === 'top' ? (
                <div
                    id={'collapse' + id}
                    data-te-collapse-item
                    data-te-collapse-show
                    aria-labelledby={'heading' + id}>
                    <div className="py-4 px-5 text-fs-primary">{content}</div>
                </div>
            ) : (
                <div
                    id={'collapse' + id}
                    data-te-collapse-item
                    aria-labelledby={'heading' + id}
                    className="hidden">
                    <div className="py-4 px-5 text-fs-primary">{content}</div>
                </div>
            )}
        </div>
    )
}
