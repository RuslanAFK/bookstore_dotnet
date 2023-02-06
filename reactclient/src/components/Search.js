const Search = ({search, setSearch}) => {
    return (
        <div className="my-3 text-center list-inline">
            <div className="list-inline-item w-25">
                <input type="searchInputValue" className="form-control"
                       onChange={($event) => setSearch($event.target.value)}
                       value={search}
                />
            </div>
        </div>
    )
}

export default Search;