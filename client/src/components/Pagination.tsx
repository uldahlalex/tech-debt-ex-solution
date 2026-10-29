type Props = {
  page: number;
  hasNextPage: boolean;
  onPageChange: (page: number) => void;
};

export function Pagination({ page, hasNextPage, onPageChange }: Props) {
  return (
    <nav className="pagination">
      <button disabled={page <= 1} onClick={() => onPageChange(page - 1)}>Previous</button>
      <span>Page {page}</span>
      <button disabled={!hasNextPage} onClick={() => onPageChange(page + 1)}>Next</button>
    </nav>
  );
}
