### Running With Docker
* cd into the solution root folder and then execute:
* docker build -f OmdbToImdb/Dockerfile -t i_tag --build-arg omdb_key=REPLACE_WITH_OMDB_API_KEY . && docker run i_tag